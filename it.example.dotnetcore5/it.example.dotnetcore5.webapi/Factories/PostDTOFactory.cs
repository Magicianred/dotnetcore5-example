using it.example.dotnetcore5.domain.Interfaces.Models;
using it.example.dotnetcore5.domain.Models;
using it.example.dotnetcore5.webapi.DTO;
using System.Collections.Generic;
using System.Linq;

namespace it.example.dotnetcore5.webapi.Factories
{
    /// <summary>
    /// Convert model domain into a DTO
    /// </summary>
    public static class PostDTOFactory
    {
        /// <summary>
        /// Covert 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static IPost ToIModelDomain(this PostDTO dto)
        {
            IPost model = null;
            if (dto != null)
            {
                model = new Post
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Text = dto.Text,
                    CreateDate = dto.CreateDate
                };
            }
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<IPost> ToIModelsDomain(this List<PostDTO> dtos)
        {
            List<IPost> models = new();
            if (dtos != null && dtos.Any())
            {
                models = new List<IPost>();
                dtos.ForEach(item =>
                {
                    models.Add(item.ToModelDomain());
                });
            }
            return models;
        }

        /// <summary>
        /// Covert 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static Post ToModelDomain(this PostDTO dto)
        {
            Post model = null;
            if (dto != null)
            {
                model = new Post
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Text = dto.Text,
                    CreateDate = dto.CreateDate
                };
            }
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<Post> ToModelsDomain(this List<PostDTO> dtos)
        {
            List<Post> models = new();
            if (dtos != null && dtos.Any())
            {
                models = new List<Post>();
                dtos.ForEach(item =>
                {
                    models.Add(item.ToModelDomain());
                });
            }
            return models;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static PostDTO ToDTO(this Post model)
        {
            PostDTO dto = null;
            if (model != null)
            {
                dto = new PostDTO
                {
                    Id = model.Id,
                    Title = model.Title,
                    Text = model.Text,
                    CreateDate = model.CreateDate
                };
            }
            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<PostDTO> ToDTOs(this List<Post> models)
        {
            List<PostDTO> dtos = new();
            if (models != null && models.Any())
            {
                dtos = new List<PostDTO>();
                models.ForEach(item =>
                {
                    dtos.Add(item.ToDTO());
                });
            }
            return dtos;
        }
    }
}