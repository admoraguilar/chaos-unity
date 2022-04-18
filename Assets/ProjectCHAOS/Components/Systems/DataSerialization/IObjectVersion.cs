
namespace ProjectCHAOS.Systems.DataSerialization
{
	public interface IObjectVersion
	{
		int objectVersion { get; }

		IObjectVersion ToPrev();
		IObjectVersion ToNext();
	}
}