using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskagerPro.Services.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderAsync<TModel>(string name, TModel model);
    }
}
