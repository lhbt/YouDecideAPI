using Nancy.TinyIoc;
using YouDecide.Domain;
using YouDecide.Mongo;

namespace YouDecideAPI
{
    using Nancy;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<IStoryNavigator, StoryNavigator>().AsMultiInstance();
            container.Register<IDataAccess, MongoDataAccess>().AsMultiInstance();
        }
    }
}