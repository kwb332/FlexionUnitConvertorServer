using Flexion.Test.Application;
using Flexion.Test.Application.ApplicationInterface;
using Flexion.Test.Domain;
using Flexion.Test.Domain.DomainInterface;
using Flexion.Test.Domain.DomainService;
using Flexion.Test.Domain.Mock;
using Flexion.Test.Infrastructure.InfrastructureInterface;
using Flexion.Test.Infrastructure.Mock.Persistence;
using Flexion.Test.Infrastructure.Mock.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;



namespace ExamUnitTest
{
    public class ExamFixer
    {
        public ServiceProvider ServiceProvider { get; set; }
        public ExamFixer()
        {
            var serviceCollection = new ServiceCollection();
       
            serviceCollection.AddTransient<ITestRepository, ExamRepositoryMock>();
            serviceCollection.AddTransient<ITestService, ExamServiceMock>();
            serviceCollection.AddTransient<IConversionService, ConversionService>();
            serviceCollection.AddDbContext<ExamMockDBContext>();
            serviceCollection.AddTransient<ITestApplicationDriver, TestApplicationDriver>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
