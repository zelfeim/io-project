using System;
using Application.Domain.Aggregates.VisitAggregate;
using Application.Domain.Aggregates.VisitAggregate.Enums;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace Core.UnitTests.Domain.Aggregates.VisitAggregate;

[TestSubject(typeof(Visit))]
public class VisitTest
{
    public static TheoryData<DateTime, int, string> RescheduleVisitTestData = new()
    {
        { DateTime.Now, 30, "Visit date must be in the future (Parameter 'date')" },
        { DateTime.Now.AddHours(1), 0, "Visit length must be greater than zero (Parameter 'visitLength')" }
    };

    [Fact]
    public void SetCancelledStatusShouldSetCancelledStatus()
    {
        // Arrange
        var visit = new Visit(0, 0, DateTime.Now.AddHours(1), VisitType.Examination, 30);

        // Act
        visit.SetCancelledStatus();

        // Assert
        visit.VisitStatus.Should().Be(VisitStatus.Cancelled);
    }

    [Fact]
    public void SetCancelledStatusShouldThrowExceptionWhenVisitIsNotPlanned()
    {
        // Arrange
        var visit = new Visit(0, 0, DateTime.Now.AddHours(1), VisitType.Examination, 30);
        visit.SetCompletedStatus();

        // Act
        var act = () => visit.SetCancelledStatus();

        // Assert
        act.Should().Throw<Exception>()
            .WithMessage("It's not possible to change visit status from Completed to Cancelled.");
    }

    [Fact]
    public void RescheduleVisitShouldRescheduleVisit()
    {
        // Arrange
        var visit = new Visit(0, 0, DateTime.Now.AddHours(1), VisitType.Examination, 30);

        // Act
        visit.RescheduleVisit(DateTime.Parse("2035-01-01"), 60);

        // Assert
        visit.VisitStatus.Should().Be(VisitStatus.Planned);
        visit.Date.Should().Be(DateTime.Parse("2035-01-01"));
        visit.VisitLength.Should().Be(60);
    }

    [Theory]
    [MemberData(nameof(RescheduleVisitTestData))]
    public void RescheduleVisitShouldThrowExceptionWhenValidationFails(DateTime date, int length,
        string expectedMessage)
    {
        // Arrange
        var visit = new Visit(0, 0, DateTime.Now.AddHours(1), VisitType.Examination, 30);

        // Act
        var act = () => visit.RescheduleVisit(date, length);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage(expectedMessage);
    }
}