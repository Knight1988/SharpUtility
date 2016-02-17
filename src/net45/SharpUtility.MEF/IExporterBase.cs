namespace SharpUtility.MEF
{
    public interface IExporterBase
    {
        string Name { get; set; }
        ExporterEventHandler ExporterEventHandler { get; set; }
    }
}
