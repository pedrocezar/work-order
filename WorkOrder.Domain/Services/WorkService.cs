﻿using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;
using WorkOrder.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace WorkOrder.Domain.Services;

public class WorkService(IWorkRepository _repository, IHttpContextAccessor _httpContextAccessor) : BaseService<WorkModel>(_repository, _httpContextAccessor), IWorkService
{
}
