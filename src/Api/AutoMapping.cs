using Api.TaskEndpoints;
using Api.UserEndpoints;
using ApplicationCore;
using AutoMapper;

namespace Api;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<CreateTaskCommand, TaskItem>();
        CreateMap<UpdateTaskCommand, TaskItem>();

        CreateMap<TaskItem, CreateTaskResult>();
        CreateMap<TaskItem, UpdatedTaskResult>();
        CreateMap<TaskItem, TaskItemDto>();
        CreateMap<TaskItem, TaskResult>();

        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();

        CreateMap<User, CreateUserResult>();
        CreateMap<User, UpdatedUserResult>();
        CreateMap<User, UserListResult>();
        CreateMap<User, UserResult>();
    }
}
