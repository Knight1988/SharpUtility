To create self installing window service.

/// <summary>
///     The main entry point for the application.
/// </summary>
private static void Main(string[] args)
{
    if (Environment.UserInteractive)
    {
        if (args.Length > 0)
            switch (args[0])
            {
                case "-i":
                case "-install":
                    WindowServiceManager.Install("MyService", "MyService", "MyService", Assembly.GetExecutingAssembly().Location);
                    break;
                case "-u":
                case "-uninstall":
                    WindowServiceManager.Uninstall("MyService");
                    break;
            }
    }
    else
    {
        ServiceBase[] servicesToRun = {new MyService()};
        ServiceBase.Run(servicesToRun);
    }
}