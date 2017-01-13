﻿using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Brandbank.Xml.ImageDownloader
{
    public class BrandbankImageDownloaderLogger : IBrandbankImageDownloader
    {
        ILogger<IBrandbankImageDownloader> _logger;
        IBrandbankImageDownloader _downloader;

        public BrandbankImageDownloaderLogger(ILogger<IBrandbankImageDownloader> logger, IBrandbankImageDownloader downloader)
        {
            _logger = logger;
            _downloader = downloader;
        }

        public Stream DownloadToStream(string url)
        {
            return Log(_downloader.DownloadToStream, url, "Stream");
        }

        public byte[] DownloadToByteArray(string url)
        {
            return Log(_downloader.DownloadToByteArray, url, "Byte Array");
        }

        private T Log<T>(Func<string, T> fn, string url, string type)
        {
            _logger.LogDebug($"Downloading image to {type} from {url}", url);
            try
            {
                var item = fn(url);
                if (item != null)
                    _logger.LogDebug($"Downloaded image as {type} from {url}", url);
                else
                    _logger.LogDebug($"Failed to download data {type} as tream from {url}", url);
                return item;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Failed to download {type} as stream from {url}", url);
                throw new Exception("BrandbankImageDownloaderLogger");
            }
        }

        
    }
}
