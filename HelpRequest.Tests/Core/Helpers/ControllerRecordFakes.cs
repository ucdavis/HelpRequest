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

        public static void FakeCatbert(int count, IRepository<CatbertApplication> catbertApplicationRepository)
        {
            var catBertApplications = new List<CatbertApplication>();
            FakeCatbert(count, catbertApplicationRepository, catBertApplications);
        }
        public static void FakeCatbert(int count, IRepository<CatbertApplication> catbertApplicationRepository, List<CatbertApplication> specificCatBertApplications)
        {
            var catBertApplications = new List<CatbertApplication>();
            var specificCatBertApplicationsCount = 0;
            if (specificCatBertApplications != null)
            {
                specificCatBertApplicationsCount = specificCatBertApplications.Count;
                for (int i = 0; i < specificCatBertApplicationsCount; i++)
                {
                    catBertApplications.Add(specificCatBertApplications[i]);
                }
            }

            for (int i = 0; i < count; i++)
            {
                catBertApplications.Add(CreateValidEntities.CatbertApplication(i + specificCatBertApplicationsCount + 1));
            }

            var totalCount = catBertApplications.Count;
            for (int i = 0; i < totalCount; i++)
            {
                catBertApplications[i].SetIdTo(i + 1);
                int i1 = i;
                catbertApplicationRepository
                    .Expect(a => a.GetNullableById(i1 + 1))
                    .Return(catBertApplications[i])
                    .Repeat
                    .Any();
            }
            catbertApplicationRepository.Expect(a => a.GetNullableById(totalCount + 1)).Return(null).Repeat.Any();
            catbertApplicationRepository.Expect(a => a.Queryable).Return(catBertApplications.AsQueryable()).Repeat.Any();
            catbertApplicationRepository.Expect(a => a.GetAll()).Return(catBertApplications).Repeat.Any();
        }


        public static void FakeUsers(int count, IRepository<User> userRepository)
        {
            var users = new List<User>();
            FakeUsers(count, userRepository, users);
        }
        public static void FakeUsers(int count, IRepository<User> userRepository, List<User> specificUsers)
        {
            var users = new List<User>();
            var specificUsersCount = 0;
            if (specificUsers != null)
            {
                specificUsersCount = specificUsers.Count;
                for (int i = 0; i < specificUsersCount; i++)
                {
                    users.Add(specificUsers[i]);
                }
            }

            for (int i = 0; i < count; i++)
            {
                users.Add(CreateValidEntities.User(i + specificUsersCount + 1));
            }

            var totalCount = users.Count;
            for (int i = 0; i < totalCount; i++)
            {
                users[i].SetIdTo(i + 1);
                int i1 = i;
                userRepository
                    .Expect(a => a.GetNullableById(i1 + 1))
                    .Return(users[i])
                    .Repeat
                    .Any();
            }
            userRepository.Expect(a => a.GetNullableById(totalCount + 1)).Return(null).Repeat.Any();
            userRepository.Expect(a => a.Queryable).Return(users.AsQueryable()).Repeat.Any();
            userRepository.Expect(a => a.GetAll()).Return(users).Repeat.Any();
        }

        public static void FakeApplication(int count, IRepository<Application> applicationRepository)
        {
            var applications = new List<Application>();
            FakeApplication(count, applicationRepository, applications);
        }
        public static void FakeApplication(int count, IRepository<Application> applicationRepository, List<Application> specificApplications)
        {
            var applications = new List<Application>();
            var specificApplicationsCount = 0;
            if (specificApplications != null)
            {
                specificApplicationsCount = specificApplications.Count;
                for (int i = 0; i < specificApplicationsCount; i++)
                {
                    applications.Add(specificApplications[i]);
                }
            }

            for (int i = 0; i < count; i++)
            {
                applications.Add(CreateValidEntities.Application(i + specificApplicationsCount + 1));
            }

            var totalCount = applications.Count;
            for (int i = 0; i < totalCount; i++)
            {
                applications[i].SetIdTo(i + 1);
                int i1 = i;
                applicationRepository
                    .Expect(a => a.GetNullableById(i1 + 1))
                    .Return(applications[i])
                    .Repeat
                    .Any();
            }
            applicationRepository.Expect(a => a.GetNullableById(totalCount + 1)).Return(null).Repeat.Any();
            applicationRepository.Expect(a => a.Queryable).Return(applications.AsQueryable()).Repeat.Any();
            applicationRepository.Expect(a => a.GetAll()).Return(applications).Repeat.Any();
        }
    }
}
