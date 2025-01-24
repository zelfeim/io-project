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
}