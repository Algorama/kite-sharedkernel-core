using System;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace SharedKernel.NHibernate.Repositories
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory()
        {
            if (_sessionFactory != null) return _sessionFactory;

            var config = new Configuration().Configure(GetPath() + "\\nhibernate.cfg.xml");
            _sessionFactory = config.BuildSessionFactory();
            return _sessionFactory;
        }

        public static string GetPath()
        {
            var x = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var path = x.LocalPath.ToLower().Replace("\\sharedkernel.nhibernate.dll", "");
            return path;
        }

        public static ISession OpenSession()
        {
            return SessionFactory().OpenSession();
        }

        public static void CreateSchema()
        {
            var caminhoArquivo = GetPath() + "\\nhibernate.cfg.xml";
            var config = new Configuration().Configure(caminhoArquivo);

            var schemaExport = new SchemaExport(config);

            Console.WriteLine("Inicio da criação da estrutura do Database!");
            schemaExport.Create(true, true);     // Gera e executa o Script de Create
            // schemaExport.Create(true, false); // Gera o Script de Create sem executar
            Console.WriteLine("Fim da criação da estrutura do database!");
        }
    }
}
