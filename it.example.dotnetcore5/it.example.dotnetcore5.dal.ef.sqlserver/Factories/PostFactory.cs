using it.example.dotnetcore5.dal.ef.sqlserver.EfModels;
using System.Collections.Generic;
using System.Linq;

using ModelPost = it.example.dotnetcore5.domain.Models.Post;

namespace it.example.dotnetcore5.dal.ef.sqlserver.Factories
{
    /// <summary>
    /// Convert Post model domain into a Post Ef entity
    /// </summary>
    public static class PostFactory
    {

        /// <summary>
        /// Covert 
        /// </summary>
        /// <param name="efModel"></param>
        /// <returns></returns>
        public static ModelPost ToModelDomain(this Post efEntity)
        {
            ModelPost model = null;
            if (efEntity != null)
            {
                model = new ModelPost();
                model.Id = efEntity.Id;
                model.Title = efEntity.Title;
                model.Text = efEntity.Text;
                model.CreateDate = efEntity.CreateDate;
            }
            return model;
        }

        public static List<ModelPost> ToModelsDomain(this List<Post> efEntities)
        {
            List<ModelPost> models = null;
            if (efEntities != null && efEntities.Any())
            {
                models = new List<ModelPost>();
                efEntities.ForEach(item =>
                {
                    models.Add(item.ToModelDomain());
                });
            }
            return models;
        }



        public static Post ToEfEntity(this ModelPost model)
        {
            Post efEntity = null;
            if (model != null)
            {
                efEntity = new Post();
                efEntity.Id = model.Id;
                efEntity.Title = model.Title;
                efEntity.Text = model.Text;
                efEntity.CreateDate = model.CreateDate;
            }
            return efEntity;
        }

        public static List<Post> ToEfEntities(this List<ModelPost> models)
        {
            List<Post> efEntities = null;
            if (models != null && models.Any())
            {
                efEntities = new List<Post>();
                models.ForEach(item =>
                {
                    efEntities.Add(item.ToEfEntity());
                });
            }
            return efEntities;
        }
    }
}
