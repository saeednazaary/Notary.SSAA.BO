using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;

namespace Notary.SSAA.BO.Coordinator.Estate.Helpers
{
    public class BaseInfoServiceHelper
    {
        IMediator _mediator;
        public BaseInfoServiceHelper(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Dictionary<string, string>> GetGeolocationOfRegistrationUnit(string[] UnitId, CancellationToken cancellationToken)
        {
            var unitList = await GetUnitById(UnitId, cancellationToken);
            Dictionary<string, string> result = new();
            if (unitList != null)
            {
                foreach (var unit in unitList.UnitList)
                {
                    result.Add(unit.Id, unit.GeoLocationId);
                }
            }
            return result;
        }
        public async Task<Dictionary<string, string>> GetGeolocationOfScriptorium(string[] scriptoriumId, CancellationToken cancellationToken)
        {
            var scriptoriumList = await GetScriptoriumById(scriptoriumId, cancellationToken);
            Dictionary<string, string> result = new();
            if (scriptoriumList != null)
            {
                foreach (var scr in scriptoriumList.ScriptoriumList)
                {
                    result.Add(scr.Id, scr.GeoLocationId);
                }
            }
            return result;
        }
        public async Task<GetUnitByIdViewModel> GetUnitById(string[] unitId, CancellationToken cancellationToken)
        {
            var result = new GetUnitByIdViewModel();
            if (unitId == null) return result;
            if (unitId.Length == 0) return result;

            var response = await _mediator.Send(new GetUnitByIdQuery(unitId), cancellationToken);
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return result;
            }
        }
        public async Task<GetScriptoriumByIdViewModel> GetScriptoriumById(string[] scriptoriumId, CancellationToken cancellationToken)
        {
            var result = new GetScriptoriumByIdViewModel();
            if (scriptoriumId == null) return result;
            if (scriptoriumId.Length == 0) return result;
            var response = await _mediator.Send(new GetScriptoriumByIdQuery(scriptoriumId), cancellationToken);
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return result;
            }
        }
        public async Task<GetGeolocationByIdViewModel> GetGeoLocationOfIran(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetGeolocationByIdQuery(Array.Empty<int>()) { FetchGeolocationOfIran = true }, cancellationToken);
            if (response != null && response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return new GetGeolocationByIdViewModel();
            }
        }
        public async Task<GetGeolocationByIdViewModel> GetGeolocationByLegacyId(string[] legacyId, CancellationToken cancellationToken)
        {
            var result = new GetGeolocationByIdViewModel();
            if (legacyId == null) return result;
            if (legacyId.Length == 0) return result;
            var response = await _mediator.Send(new GetGeolocationByLegacyIdQuery(legacyId), cancellationToken);
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return result;
            }

        }
        public async Task<OrganizationViewModel> GetOrganizationByScriptoriumId(string[] scriptoriumId, CancellationToken cancellationToken)
        {
            var result = new OrganizationViewModel();
            if (scriptoriumId == null) return result;
            if (scriptoriumId.Length == 0) return result;
            var response = await _mediator.Send(new GetOrganizationByScriptoriumIdQuery(scriptoriumId), cancellationToken);
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return result;
            }
        }
        public async Task<OrganizationViewModel> GetOrganizationByUnitId(string[] unitId, CancellationToken cancellationToken)
        {
            var result = new OrganizationViewModel();
            if (unitId == null) return result;
            if (unitId.Length == 0) return result;
            var response = await _mediator.Send(new GetOrganizationByScriptoriumIdQuery(unitId), cancellationToken);
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return result;
            }

        }
        public async Task<MeasurementUnitTypeByIdViewModel> GetMeasurementUnitTypeByLegacyId(string[] legacyId, CancellationToken cancellationToken)
        {
            var result = new MeasurementUnitTypeByIdViewModel();
            if (legacyId == null) return result;
            if (legacyId.Length == 0) return result;
            var response = await _mediator.Send(new MeasurementUnitTypeByLegacyIdQuery(legacyId), cancellationToken);
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return result;
            }
        }
        public async Task<OrganizationViewModel> GetOrganizationByLegacyId(string[] legacyId, CancellationToken cancellationToken)
        {
            var result = new OrganizationViewModel();
            if (legacyId == null) return result;
            if (legacyId.Length == 0) return result;
            var response = await _mediator.Send(new GetOrganizationByLegacyIdQuery(legacyId), cancellationToken);
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return result;
            }

        }
        public async Task<GetUnitByIdViewModel> GetUnitByLegacyId(string[] legacyId, CancellationToken cancellationToken)
        {
            var result = new GetUnitByIdViewModel();
            if (legacyId == null) return result;
            if (legacyId.Length == 0) return result;
            var response = await _mediator.Send(new GetUnitByLegacyIdQuery(legacyId), cancellationToken);
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return result;
            }
        }
        public async Task<GetGeolocationByIdViewModel> GetGeoLocationById(int[] geoLocationId, CancellationToken cancellationToken)
        {
            var result = new GetGeolocationByIdViewModel();
            if (geoLocationId == null) return result;
            if (geoLocationId.Length == 0) return result;
            var response = await _mediator.Send(new GetGeolocationByIdQuery(geoLocationId), cancellationToken);
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return result;
            }
        }

        public async Task<MeasurementUnitTypeByIdViewModel> GetMeasurementUnitTypeById(string[] Id, CancellationToken cancellationToken)
        {
            var result = new MeasurementUnitTypeByIdViewModel();
            if (Id == null) return result;
            if (Id.Length == 0) return result;
            var response = await _mediator.Send(new MeasurementUnitTypeByIdQuery(Id), cancellationToken);
            if (response != null && response.IsSuccess && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return result;
            }
        }
    }
}
