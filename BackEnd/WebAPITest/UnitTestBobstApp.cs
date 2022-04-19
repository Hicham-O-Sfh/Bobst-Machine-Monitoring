using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Repository.Data.Models;
using Service.DTOs;
using Service.Interfaces;
using WebAPI.Controllers;
using Xunit;

namespace WebAPITest;

public class UnitTestBobstApp
{
    public Task<List<MachineDTO>> GetFakeMachines()
    {
        return Task.FromResult(
            (List<MachineDTO>)new List<MachineDTO>
            {
                new MachineDTO{ MachineId= 1, Name="test machine 1", Description = "Description 1"},
                new MachineDTO{ MachineId= 2, Name="test machine 2", Description = "Description 2"}
            }
        );
    }

    public int GetFakeMachinesProductions()
    {
        List<MachineProduction> machinesProductions = new List<MachineProduction>();
        for (int i = 1; i < 6; i++)
            machinesProductions.Add(new MachineProduction
            {
                MachineProductionId = i,
                MachineId = 1,
                TotalProduction = 50
            });
        int Sum = machinesProductions
                    .Where(x => x.MachineId == 1)
                    .Sum(e => e.TotalProduction);
        return Sum;
    }

    [Fact]
    public void GetMachines_Should_Return_Correct_Count_Of_Data()
    {
        // mocking the required dependencie (service) 
        var fakeService = new Mock<IMachineService>();
        // setup to verify later the intended value
        fakeService
            .Setup(s => s.GetMachines())
            .Returns(this.GetFakeMachines());

        // getting instance of the Controller by giving it the required dependencies ..
        var controller = new MachinesController(fakeService.Object);
        // calling the controller's action and retrieving its result
        var machines = controller.GetMachines().Result;

        // implicitly casting the result's status for verifying its value
        var result = (OkObjectResult)machines.Result;
        // implicitly casting the result's status for verifying its value
        var collection = (List<MachineDTO>)result.Value;

        // should have status code : 200
        Assert.IsType<OkObjectResult>(result);
        // should return exactly 2 results (2 machines)
        Assert.Equal(collection.Count, 2);
    }

    [Fact]
    public void GetMachines_Should_Return_No_Content()
    {
        // mocking an empty Collection, for comparing purposes later
        var fakeEmptyList = Task.FromResult(new List<MachineDTO>());

        var fakeService = new Mock<IMachineService>();
        fakeService
            .Setup(service => service.GetMachines())
            .Returns(fakeEmptyList);

        var controller = new MachinesController(fakeService.Object);
        var machines = controller.GetMachines().Result;

        var result = (NoContentResult)machines.Result;

        // should have status code : 204
        // should return exactly no results (machines)
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void GetMachine_Should_Return_Single_Machine()
    {
        var fakeMachine = new MachineDTO()
        {
            MachineId = 1,
            Name = "Machine just for test",
            Description = "Description just for test"
        };

        var fakeService = new Mock<IMachineService>();
        fakeService
            .Setup(service => service.GetMachineById(1))
            .Returns(Task.FromResult(fakeMachine));

        var controller = new MachinesController(fakeService.Object);
        var machine = controller.GetMachine(1).Result;

        var result = (OkObjectResult)machine.Result;
        var myMachineResult = (MachineDTO)result.Value;

        // should have status code : 200
        Assert.IsType<OkObjectResult>(result);
        // should find and return exactly 1 result (1 machine)
        Assert.Equal(fakeMachine, myMachineResult);
    }

    [Fact]
    public void GetMachine_Should_Return_Not_Found()
    {
        // mocking a fake id
        // making sure that it's not present on the DataBase ..
        int id = -1;

        var fakeService = new Mock<IMachineService>();
        fakeService
            .Setup(service => service.GetMachineById(id))
            .Returns(Task.FromResult((MachineDTO)null));

        var controller = new MachinesController(fakeService.Object);
        var machine = controller.GetMachine(id);

        var result = (NotFoundResult)machine.Result.Result;

        // should have status code : 404
        // should return exactly not found
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void GetMachineProduction_Should_Return_Int_Value()
    {
        int totalProductions = this.GetFakeMachinesProductions();
        var fakeMachine = new MachineDTO() { MachineId = 1 };
        var fakeService = new Mock<IMachineService>();

        fakeService
            .Setup(service => service.GetMachineById(1))
            .Returns(Task.FromResult(fakeMachine));
        fakeService
            .Setup(service => service.GetProductionMachineById(1))
            .Returns(Task.FromResult(totalProductions));

        var controller = new MachinesController(fakeService.Object);

        var _totalProductions = controller.GetMachineProduction(1);
        var result = (OkObjectResult)_totalProductions.Result;
        var value = result.Value;

        // should have status code : 200
        Assert.IsType<OkObjectResult>(result);

        // should return an anonymous type 
        // with an Int32 "totalproduction" property inside of it
        Assert.IsType<int>(
            // retrieving the property and its value from the anonymous type (jObject)
            value.GetType().GetProperty("totalproduction").GetValue(value, null)
        );
    }

    [Fact]
    public void GetMachineProduction_Should_Return_Not_Found()
    {
        var totalProducion = -1;
        var fakeService = new Mock<IMachineService>();

        fakeService
            .Setup(service => service.GetProductionMachineById(1))
            .Returns(Task.FromResult(totalProducion));

        var controller = new MachinesController(fakeService.Object);
        var machineTotalProduction = controller.GetMachineProduction(1).Result;
        var result = (NotFoundResult)machineTotalProduction;

        // should have status code : 404
        Assert.IsType<NotFoundResult>(result);
    }

}
