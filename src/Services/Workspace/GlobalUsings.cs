global using System.ComponentModel.DataAnnotations;
global using Microsoft.EntityFrameworkCore;
global using DatabaseMonitoring.Services.Workspace.Infrustructure.DataAccess;
global using DatabaseMonitoring.Services.Workspace.Core.Domain.Entity;
global using DatabaseMonitoring.Services.Workspace.Core.Abstraction;
global using DatabaseMonitoring.Services.Workspace.Services.Contract;
global using Microsoft.AspNetCore.Mvc;
global using DatabaseMonitoring.Services.Workspace.Services.Abstraction;
global using DatabaseMonitoring.Services.Workspace.ViewModels;
global using AutoMapper;
global using DatabaseMonitoring.Services.Workspace.Mapping;
global using Microsoft.OpenApi.Models;
global using System.Reflection;
global using DatabaseMonitoring.Services.Workspace.Infrustructure.Repository.Implementation;
global using DatabaseMonitoring.Services.Workspace.Services.Implementation;
global using DatabaseMonitoring.Services.Workspace.Extensions;
global using DatabaseMonitoring.Services.Workspace.Infrustructure.Repository;
global using DatabaseMonitoring.BuildingBlocks.EventBus.Events;
global using DatabaseMonitoring.BuildingBlocks.EventBus;
global using DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;
global using DatabaseMonitoring.BuildingBlocks.EventBusRabbitMQ;
global using RabbitMQ.Client;
global using DatabaseMonitoring.Services.Workspace.ApplicationEvents.Events;
global using DatabaseMonitoring.Services.Workspace.ApplicationEvents;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using Microsoft.AspNetCore.Authorization;