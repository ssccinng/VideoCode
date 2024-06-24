UploadFile uploadFile = new UploadFile();
TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
Task task;
uploadFile.UploadCompleted += () =>
{
    tcs.SetResult(true);
};

uploadFile.Upload();

var result = await tcs.Task;

Console.WriteLine("Upload completed");

class UploadFile
{
    public Action UploadCompleted { get; set; }
    public void Upload()
    {
        Thread.Sleep(1000);

        UploadCompleted?.Invoke();

    }
}