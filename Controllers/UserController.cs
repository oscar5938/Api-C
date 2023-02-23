using AutoMapper;
using Api.Models;
using Api.Services;

namespace Api.Controllers{
    public class UserController : GenericController<Users, Model>{
        public UserController(IServices<Model> repository, IMapper mapper) :base(repository, mapper){}
    }
}