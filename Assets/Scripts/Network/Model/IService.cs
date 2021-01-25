using RSG;

namespace Assets.Scripts.Network.Model
{
    public abstract class IService
    {

        public abstract IPromise<string> Get(string api);

    }
}