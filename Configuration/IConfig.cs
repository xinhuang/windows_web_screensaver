namespace WebScreenSaver.Configuration
{
    interface IConfig
    {
        IConfigTab CreateConfigTab();
        IDataSource DataSource { get; }
    }
}
