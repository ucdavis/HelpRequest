using System;
using System.Collections.Generic;
using System.Linq;
using HelpRequest.Core.Domain;
using Rhino.Mocks;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Testing;
namespace HelpRequest.Tests.Core.Helpers
{
    public static class ControllerRecordFakes
    {
        /// <summary>
        /// Fakes the fake help topic.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="helpTopicRepository">The help topic repository.</param>
        public static void FakeHelpTopic(int count, IRepository<HelpTopic> helpTopicRepository)
        {
            var helpTopics = new List<HelpTopic>();
            FakeHelpTopic(count, helpTopicRepository, helpTopics);
        }
        /// <summary>
        /// Fakes the fake help topic.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="helpTopicRepository">The help topic repository.</param>
        /// <param name="specificHelpTopics">The specific help topics.</param>
        public static void FakeHelpTopic(int count, IRepository<HelpTopic> helpTopicRepository, List<HelpTopic> specificHelpTopics)
        {
            var helpTopics = new List<HelpTopic>();
            var specificHelpTopicsCount = 0;
            if (specificHelpTopics != null)
            {
                specificHelpTopicsCount = specificHelpTopics.Count;
                for (int i = 0; i < specificHelpTopicsCount; i++)
                {
                    helpTopics.Add(specificHelpTopics[i]);
                }
            }

            for (int i = 0; i < count; i++)
            {
                helpTopics.Add(CreateValidEntities.HelpTopic(i + specificHelpTopicsCount + 1));
            }

            var totalCount = helpTopics.Count;
            for (int i = 0; i < totalCount; i++)
            {
                helpTopics[i].SetIdTo(i + 1);
                int i1 = i;
                helpTopicRepository
                    .Expect(a => a.GetNullableById(i1 + 1))
                    .Return(helpTopics[i])
                    .Repeat
                    .Any();
            }
            helpTopicRepository.Expect(a => a.GetNullableById(totalCount + 1)).Return(null).Repeat.Any();
            helpTopicRepository.Expect(a => a.Queryable).Return(helpTopics.AsQueryable()).Repeat.Any();
            helpTopicRepository.Expect(a => a.GetAll()).Return(helpTopics).Repeat.Any();
        }               
    }
}
