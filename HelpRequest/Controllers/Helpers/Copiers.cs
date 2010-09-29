using HelpRequest.Core.Domain;

namespace HelpRequest.Controllers.Helpers
{
    public static class Copiers
    {
        /// <summary>
        /// Copies editable values
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        /// <returns>HelpTopic Destination with copy-able values from source</returns>
        public static HelpTopic HelpTopic(HelpTopic destination, HelpTopic source)
        {
            destination.Question = source.Question;
            destination.Answer = source.Answer;
            destination.IsActive = source.IsActive;
            destination.NumberOfReads = source.NumberOfReads;
            destination.IsVideo = source.IsVideo;
            destination.VideoName = source.VideoName;
            destination.AvailableToPublic = source.AvailableToPublic;
            destination.AppFilter = source.AppFilter;

            return destination;
        }
    }
}
