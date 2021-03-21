using LoginExample.Data.Model;


namespace LoginExample.Data {
public interface IUserService {
    User ValidateUser(string userName, string password);
}
}