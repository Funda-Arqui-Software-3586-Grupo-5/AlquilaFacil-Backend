using LocalManagement.Domain.Model.Aggregates;
using LocalManagement.Domain.Model.Commands;
using LocalManagement.Domain.Repositories;
using LocalManagement.Domain.Services;
using LocalManagement.Shared.Domain.Repositories;

namespace LocalManagement.Application.Internal.CommandServices;

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