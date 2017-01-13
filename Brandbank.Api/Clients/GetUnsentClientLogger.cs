﻿using Brandbank.Xml.Models.Message;
using Microsoft.Extensions.Logging;
using System;
using System.Xml;
using System.Xml.Linq;

namespace Brandbank.Api.Clients
{
    public class GetUnsentClientLogger : IGetUnsentClient
    {
        private readonly ILogger<IGetUnsentClient> _logger;
        private readonly IGetUnsentClient _getUnsentClient;

        public GetUnsentClientLogger(ILogger<IGetUnsentClient> logger, IGetUnsentClient uploadClient)
        {
            _logger = logger;
            _getUnsentClient = uploadClient;
        }

        public IBrandbankMessageSummary AcknowledgeMessage(IBrandbankMessageSummary messageInformation)
        {
            _logger.LogDebug($"Acknowledging Brandbank message");
            try
            {
                var result = _getUnsentClient.AcknowledgeMessage(messageInformation);
                _logger.LogDebug($"Acknowledged Brandbank message");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Acknowledging brandbank message failed: {e.Message}");
                throw new Exception();
            }
        }

        public XmlNode GetUnsentProductData()
        {
            _logger.LogDebug($"Getting Unsent Brandbank data");
            try
            {
                var result = _getUnsentClient.GetUnsentProductData();
                _logger.LogDebug($"Got Unsent Brandbank data");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Getting Unsent Brandbank data failed: {e.Message}");
                throw new Exception("Getting Unsent Brandbank data failed", e);
            }
        }

        public void Dispose()
        {
            _logger.LogDebug($"Disposing unsent client");
            try
            {
                _getUnsentClient.Dispose();
                _logger.LogDebug($"Disposed unsent client");
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Disposing unsent client failed: {e.Message}");
                throw new Exception("Disposing unsent client failed", e);
            }
        }
    }
}
