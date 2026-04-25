using Moq;
using TournamentApp.Application.DTOs;
using TournamentApp.Application.Services;
using TournamentApp.Domain.Entities;
using TournamentApp.Domain.Interfaces;

namespace TournamentApp.UnitTests;

public class TournamentServiceTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsTournamentDto_WhenTournamentExists()
    {
        // Arrange
        var mockRepo = new Mock<IGenericRepository<Tournament>>();
        var tournamentId = Guid.NewGuid();
        var tournament = new Tournament { Id = tournamentId, Name = "Test Tournament" };
        
        mockRepo.Setup(repo => repo.GetByIdAsync(tournamentId)).ReturnsAsync(tournament);
        
        var service = new TournamentService(mockRepo.Object);

        // Act
        var result = await service.GetByIdAsync(tournamentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tournamentId, result.Id);
        Assert.Equal("Test Tournament", result.Name);
    }
}
