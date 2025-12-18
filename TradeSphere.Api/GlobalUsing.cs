global using TradeSphere.Api.Errors;
global using TradeSphere.Api.Middlewares;
global using System.Text.Json;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using System;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.AspNetCore.Builder;
global using System.Linq;
global using TradeSphere.Api.Extensions;
global using Microsoft.EntityFrameworkCore;
global using TradeSphere.Infrastructure.Persistence.DbContext;
global using TradeSphere.Infrastructure.Persistence.SeedDataClass;
global using Microsoft.Extensions.Configuration;
global using Microsoft.AspNetCore.Mvc;
global using TradeSphere.Application.DTOs.AuthDto;
global using TradeSphere.Application.UseCases;
global using TradeSphere.Application.Interfaces.Repositories;
global using TradeSphere.Application.Interfaces.Services;
global using TradeSphere.Domain.Models.IdentityUser;
global using TradeSphere.Infrastructure.Repositories.AuthRepository;
global using TradeSphere.Infrastructure.Services;
global using Microsoft.AspNetCore.Authorization;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using System.Text;
global using Microsoft.AspNetCore.Identity;






