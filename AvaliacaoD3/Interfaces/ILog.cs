using AvaliacaoD3.Models;

namespace AvaliacaoD3.Interfaces
{
    internal interface ILog
    {
        void RegisterLogin(User user);

        void RegisterLogout(User user);
    }
}
