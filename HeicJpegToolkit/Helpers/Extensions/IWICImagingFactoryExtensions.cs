﻿using HeicJpegToolkit.Helpers.Enums;
using HeicJpegToolkit.Interfaces;
using HeicJpegToolkit.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static class IWICImagingFactoryExtensions
    {
        public static IWICBitmap CreateBitmap(this IWICImagingFactory imagingFactory, WICSize size, Guid pixelFormat, WICBitmapCreateCacheOption option)
        {
            return imagingFactory.CreateBitmap(size.Width, size.Height, pixelFormat, option);
        }

        public static IWICBitmap CreateBitmapFromSourceRect(this IWICImagingFactory imagingFactory, IWICBitmapSource pIBitmapSource, WICRect rect)
        {
            return imagingFactory.CreateBitmapFromSourceRect(pIBitmapSource, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static IWICBitmap CreateBitmapFromMemory(this IWICImagingFactory imagingFactory, WICSize size, Guid pixelFormat, int cbStride, byte[] pbBuffer)
        {
            return imagingFactory.CreateBitmapFromMemory(size.Width, size.Height, pixelFormat, cbStride, pbBuffer.Length, pbBuffer);
        }

        //public static IWICBitmapDecoder CreateDecoder(this IWICImagingFactory imagingFactory, Guid guidContainerFormat, Guid pguidVendor = default)
        //{
        //    //using (var pguidVendorPtr = CoTaskMemPtr.From(pguidVendor))
        //    {
        //        return imagingFactory.CreateDecoder(guidContainerFormat, pguidVendor);
        //    }
        //}

        public static IWICBitmapDecoder CreateDecoderFromFileHandle(this IWICImagingFactory imagingFactory, IntPtr hFile, WICDecodeOptions metadataOptions, Guid pguidVendor = default)
        {
            //using (var pguidVendorPtr = CoTaskMemPtr.From(pguidVendor))
            {
                return imagingFactory.CreateDecoderFromFileHandle(hFile, pguidVendor, metadataOptions);
            }
        }

        public static IWICBitmapDecoder CreateDecoderFromFilename(this IWICImagingFactory imagingFactory, string wzFilename, Guid pguidVendor = default, WICDecodeOptions metadataOptions = WICDecodeOptions.WICDecodeMetadataCacheOnDemand)
        {
            //using (var pguidVendorPtr = CoTaskMemPtr.From(pguidVendor))
            {
                return imagingFactory.CreateDecoderFromFilename(wzFilename, pguidVendor, StreamAccessMode.GENERIC_READ, metadataOptions);
            }
        }

        public static IWICBitmapDecoder CreateDecoderFromStream(this IWICImagingFactory imagingFactory, IStream stream, WICDecodeOptions metadataOptions, Guid pguidVendor = default)
        {
            //using (var pguidVendorPtr = CoTaskMemPtr.From(pguidVendor))
            {
                return imagingFactory.CreateDecoderFromStream(stream, pguidVendor, metadataOptions);
            }
        }

        public static IWICBitmapDecoder CreateDecoderFromStream(this IWICImagingFactory imagingFactory, Stream stream, WICDecodeOptions metadataOptions, Guid pguidVendor = default)
        {
            return imagingFactory.CreateDecoderFromStream(stream.AsCOMStream(), metadataOptions, pguidVendor);
        }      
    }
}

