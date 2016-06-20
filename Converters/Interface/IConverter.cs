namespace UniversalConverter
{
    public interface IConverter
    {
        bool TryConvert(ConverterContext context, out object result);
    }
}
