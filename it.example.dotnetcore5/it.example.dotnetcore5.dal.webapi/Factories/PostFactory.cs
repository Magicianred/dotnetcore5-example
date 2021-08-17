using it.example.dotnetcore5.dal.webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using ModelPost = it.example.dotnetcore5.domain.Models.Post;

namespace it.example.dotnetcore5.dal.webapi.Factories
{
    /// <summary>
    /// Convert Post model domain into a web api model
    /// </summary>
    public static class PostFactory
    {
        /// <summary>
        /// Covert 
        /// </summary>
        /// <param name="webModel"></param>
        /// <returns></returns>
        public static ModelPost ToModelDomain(this WebPost webModel)
        {
            ModelPost model = null;
            if (webModel != null)
            {
                model = new ModelPost
                {
                    Id = (int)webModel.Id,
                    Title = webModel.Title,
                    Text = webModel.Body,
                    CreateDate = DateTime.Now
                };
            }
            return model;
        }

        public static List<ModelPost> ToModelsDomain(this List<WebPost> webModels)
        {
            List<ModelPost> models = null;
            if (webModels != null && webModels.Any())
            {
                models = new List<ModelPost>();
                webModels.ForEach(item =>
                {
                    models.Add(item.ToModelDomain());
                });
            }
            return models;
        }



        public static WebPost ToWebModel(this ModelPost model)
        {
            WebPost webModel = null;
            if (model != null)
            {
                webModel = new WebPost
                {
                    Id = model.Id,
                    Title = model.Title,
                    Body = model.Text
                };
            }
            return webModel;
        }

        public static List<WebPost> ToWebModels(this List<ModelPost> models)
        {
            List<WebPost> webModels = null;
            if (models != null && models.Any())
            {
                webModels = new List<WebPost>();
                models.ForEach(item =>
                {
                    webModels.Add(item.ToWebModel());
                });
            }
            return webModels;
        }
    }
}
