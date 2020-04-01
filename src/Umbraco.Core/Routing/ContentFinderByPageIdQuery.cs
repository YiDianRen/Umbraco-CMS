﻿using Umbraco.Core.Request;

namespace Umbraco.Web.Routing
{
    /// <summary>
    /// This looks up a document by checking for the umbPageId of a request/query string
    /// </summary>
    /// <remarks>
    /// This is used by library.RenderTemplate and also some of the macro rendering functionality like in
    /// macroResultWrapper.aspx
    /// </remarks>
    public class ContentFinderByPageIdQuery : IContentFinder
    {
        private readonly IRequestAccessor _requestAccessor;

        public ContentFinderByPageIdQuery(IRequestAccessor requestAccessor)
        {
            _requestAccessor = requestAccessor;
        }

        public bool TryFindContent(IPublishedRequest frequest)
        {
            int pageId;
            if (int.TryParse(_requestAccessor.GetRequestValue("umbPageID"), out pageId))
            {
                var doc = frequest.UmbracoContext.Content.GetById(pageId);

                if (doc != null)
                {
                    frequest.PublishedContent = doc;
                    return true;
                }
            }
            return false;
        }
    }
}