Lock lockObj = new Lock();

var lock1 = lockObj.EnterScope();
lock1.Dispose();

List<ReadOnlyMemory<byte>> data = new List<ReadOnlyMemory<byte>>();
List<ReadOnlySpan<byte>> data1= new List<ReadOnlySpan<byte>>();