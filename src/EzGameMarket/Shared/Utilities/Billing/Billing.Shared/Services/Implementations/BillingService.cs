﻿using EventBus.Shared.Abstraction;
using Shared.Utilities.Billing.Shared.IntegrationEvents.Events.Create;
using Shared.Utilities.Billing.Shared.IntegrationEvents.Events.Storno;
using Shared.Utilities.Billing.Shared.Services.Abstractions;
using Shared.Utilities.Billing.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Billing.Shared.Services.Implementations
{
    public class BillingService : IBillingService
    {
        private IBillingRepository _billingRepository;
        private IEventBusRepository _eventBus;

        public BillingService(IBillingRepository billingRepository, IEventBusRepository eventBus)
        {
            _billingRepository = billingRepository;
            _eventBus = eventBus;
        }

        public async Task CreateInvoiceAsync(BillViewModel model)
        {
            if (model != default)
            {
                try
                {
                    await _billingRepository.Bill(model);

                    _eventBus.Publish(new BillingSuccessIntegrationEvent());
                }
                catch (Exception ex)
                {
                    _eventBus.Publish(new BillingFailedIntegrationEvent(ex));

                    throw;
                }
            }
        }

        public Task<IEnumerable<BillViewModel>> GetAll() => _billingRepository.GetAll();

        public Task<BillViewModel> GetByID(string id) => _billingRepository.GetByID(id);

        public Task<IEnumerable<BillViewModel>> GetByIDs(IEnumerable<string> ids) => _billingRepository.GetByIDs(ids);

        public async Task Strono(string id)
        {
            if (string.IsNullOrEmpty(id) == default)
            {
                try
                {
                    await _billingRepository.Storno(id);

                    _eventBus.Publish(new BillStornoSuccessfullIntegrationEvent());
                }
                catch (Exception ex)
                {
                    _eventBus.Publish(new BillStornoFailedIntegrationEvent(ex));

                    throw;
                }
            }
        }
    }
}