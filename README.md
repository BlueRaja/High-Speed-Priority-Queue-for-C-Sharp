# High Speed Priority Queue for C&#35;

### Features ###
* Faster (for path-finding, at least) than any other C# priority queue out there!
* Easy to use
* No dependencies on third-party libraries
* Free for both personal and commercial use.
* Implements `IEnumerable<T>` for LINQ support!
* Is a **stable priority queue** - that is, if two items are are enqueued with the same priority, they'll be dequeued in the same order they were enqueued.
* Takes advantage of the new [forced inline support](http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.methodimploptions%28v=vs.110%29.aspx) when compiling under .Net 4.5, for even faster speeds.
* Should work on .Net versions as old as .Net 2.0

### Is this software free? ###

**Yes!**  See the [license page](https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp/wiki/License) for more details.

### How do I use the priority queue? ###

Check out the [Getting Started](https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp/wiki/Getting-Started) page for a quick tutorial.  Or, take a look at the [Priority Queue Example](https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp/blob/master/Priority%20Queue%20Example/Program.cs) project in the repository.
