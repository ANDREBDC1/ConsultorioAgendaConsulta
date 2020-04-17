using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultorio_Web.AutoMapper
{
    public class AutoMapperConfing
    {
        public static void RegistarMapiamento()
        {
            Mapper.Initialize(c =>
            {
                c.AddProfile<DominioParaViewModelMapiarPreferencia>();
                c.AddProfile<ViewModelParaDominioMapiarPreferencia>();
            });
        }
    }
}