/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 01.10.2026
 * Time: 18:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;
using System.Threading;

public sealed class BufferedFileWriter : IDisposable {
    private readonly object _sync = new object();
    private readonly int _timeoutMs;

    private FileStream _stream;
    private Timer _timer;
    private bool _disposed;

    public string FileName = "";
    
    public BufferedFileWriter(
        string fileName,
        int timeoutMs) {
        if ( string.IsNullOrEmpty( fileName ) ) {
            throw new ArgumentNullException("fileName");
        }

        if ( timeoutMs <= 0 ) {
            throw new ArgumentOutOfRangeException("timeoutMs");
        }

        _timeoutMs = timeoutMs;
        
        _stream = new FileStream(
            fileName,
            FileMode.Append,
            FileAccess.Write,
            FileShare.Read,
            10 * 1024,
            FileOptions.SequentialScan);

        _timer = new Timer(
            TimerCallback,
            null,
            _timeoutMs,
            _timeoutMs);
        FileName = fileName;
    }

    public void Write ( byte[] data ) {
        if ( data == null ) {
            throw new ArgumentNullException("data");
        }

        Write( data, 0, data.Length );
    }

    public void Write ( byte[] data, int offset, int count ) {
        if ( data == null ) {
            throw new ArgumentNullException("data");
        }

        if ( offset < 0 || offset > data.Length ) {
            throw new ArgumentOutOfRangeException("offset");
        }

        if ( count < 0 || offset + count > data.Length ) {
            throw new ArgumentOutOfRangeException("count");
        }

        lock ( _sync ) {
            CheckDisposed();

            _stream.Write( data, offset, count );
        }
    }

    public void Flush () {
        lock ( _sync ) {
            CheckDisposed();

            _stream.Flush();
        }
    }

    private void TimerCallback ( object state ) {
        lock ( _sync ) {
            if ( _disposed ) {
                return;
            }

            _stream.Flush();
        }
    }

    private void CheckDisposed () {
        if ( _disposed ) {
            throw new ObjectDisposedException("BufferedFileWriter");
        }
    }

    public void Dispose () {
        lock ( _sync ) {
            if ( _disposed ) {
                return;
            }

            _disposed = true;

            if ( _timer != null ) {
                _timer.Dispose();
                _timer = null;
            }

            _stream.Flush();
            _stream.Dispose();
            _stream = null;
        }
    }
}
