using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlueLight.Main;

namespace BlueLight.Main
{
    public class ReceiveLogCaptureService
    {
        private readonly LoggingContext _loggingContext;

        public ReceiveLogCaptureService(LoggingContext loggingContext)
        {
            _loggingContext = loggingContext;

        }

        public async Task SaveAddress(Guid sessionId,
        List<string> address_items, string event1)
        {
            var address = new LogReceiveAddress()
            {
                SessionId = sessionId,
                EventType = event1
            };

            address.Details = address.Details ?? new List<LogReceiveAddressDetail>();
            foreach (var item in address_items)
            {
                var logDetail = new LogReceiveAddressDetail()
                {
                    LogReceiveAddress = address,
                    Address = item
                };
                address.Details.Add(logDetail);

            }
            await this._loggingContext.LogReceiveAddresses.AddAsync(address);
            await _loggingContext.SaveChangesAsync();
        }

        public async Task SaveRaw(Guid sessionId, string content, string event1)
        {
            var raw = new LogReceiveRaw()
            {
                SessionId = sessionId,
                EventType = event1,
                Content = content
            };

            await this._loggingContext.LogReceiveRaws.AddAsync(raw);
            await _loggingContext.SaveChangesAsync();
        }
    }



}
