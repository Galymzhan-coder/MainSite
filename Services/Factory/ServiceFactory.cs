using Models.DTO;
using Models.DTO.Interfaces;
using Services.Factory.Interfaces;
using Services.FND.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Factory
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, (string, Type, Type, string)> _serviceTypes = new Dictionary<string, (string, Type, Type, string)>
        {
            { "category",        ("categories", typeof(ICategoriesService), typeof(CategoryDTO), "Категория") },
            { "text_pages",      ("pages", typeof(IPagesService), typeof(PagesDTO), "Статьи") },
            { "contents",        ("content", typeof(IContentService), typeof(ContentDTO), "Текстовые страницы") },
            { "language",        ("languages", typeof(ILanguagesService), typeof(LanguagesDTO), "Язык") },
            { "blog",            ("blogs", typeof(IBlogsService), typeof(BlogsDTO), "Записи блога") },
            { "blog_ctg",        ("blog_categories", typeof(IBlogCategoriesService), typeof(BlogCategoriesDTO), "Категории блога") },
            { "top_managements", ("top_management", typeof(ITopManagementService), typeof(TopManagementDTO), "Топ-менеджеры") },
            { "faq_category",    ("faq_categories", typeof(IFaqCategoriesService), typeof(FaqCategoriesDTO), "Категории вопрос-ответ") }
        };

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public dynamic GetService(string type)
        {
            if (_serviceTypes.TryGetValue(type.ToLower(), out var serviceInfo))
            {
                var service = _serviceProvider.GetService(serviceInfo.Item2);
                if (service != null)
                {
                    return (dynamic)service;
                }
            }
            return null;
        }

        public Type GetInterface(string type)
        {
           return _serviceTypes[type].Item2;
        }

        public Type GetClass(string type)
        {
            return _serviceTypes[type].Item3;
        }

        public List<string> GetServiceTypes()
        {
            return _serviceTypes.Select(k => $"{k.Key} - {k.Value.Item4}").ToList();
        }
    }
}
