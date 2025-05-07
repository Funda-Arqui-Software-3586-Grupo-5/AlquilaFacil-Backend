using AlquilaFacilPlatform.Booking.Domain.Model.Commands;
using AlquilaFacilPlatform.Locals.Domain.Model.Aggregates;
using AlquilaFacilPlatform.Locals.Domain.Model.Commands;
using AlquilaFacilPlatform.Locals.Domain.Repositories;
using AlquilaFacilPlatform.Locals.Domain.Services;
using AlquilaFacilPlatform.Shared.Domain.Repositories;

namespace AlquilaFacilPlatform.Locals.Application.Internal.CommandServices;

public class ReportCommandService (IReportRepository reportRepository, IUnitOfWork unitOfWork) : IReportCommandService
{
    public async Task<Report?> Handle(CreateReportCommand command)
    {
        var report = new Report(command);
        await reportRepository.AddAsync(report);
        await unitOfWork.CompleteAsync();
        return report;
    }

    public async Task<Report?> Handle(DeleteReportCommand command)
    {
        var reportToDelete =  await reportRepository.FindByIdAsync(command.Id);
        if (reportToDelete == null)
        {
            throw new Exception("Report not found");
        }
        reportRepository.Remove(reportToDelete);
        await unitOfWork.CompleteAsync();
        return reportToDelete;
    }
}