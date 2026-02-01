using HelloKenzo.Web.Models;

namespace HelloKenzo.Web.Interfaces;

public interface IRegistrationService
{
    bool Register(RegistrationRequest request);
}
