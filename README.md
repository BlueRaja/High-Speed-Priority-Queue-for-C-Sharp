# High Speed Priority Queue for C&#35;

### Features ###
* Faster (for path-finding, at least) than any other C# priority queue out there!
* Easy to use
* No dependencies on third-party libraries
* Free for both personal and commercial use
* Implements `IEnumerable<T>` for LINQ support!
* Fully unit-tested
* Is a **stable priority queue** - that is, if two items are are enqueued with the same priority, they'll be dequeued in the same order they were enqueued
* Takes advantage of the new [forced inline support](http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.methodimploptions%28v=vs.110%29.aspx) when compiling under .Net 4.5, for even faster speeds
* Published to [NuGet](https://www.nuget.org/packages/OptimizedPriorityQueue/) - can [easily be added to any project](https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp/wiki/Getting-Started)
* Should work on .Net versions as old as .Net 2.0

### Is this software free? ###

**Yes!**  See the [license page](https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp/wiki/License) for more details.

### Getting Started ###

This project contains two priority queue implementations - one that's super-fast _(without thread-safety, safety checks, etc)_, and one that's easy/safe to use.

See the [Getting Started](https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp/wiki/Getting-Started) page, or decide what type of queue you want:

* [I want a PriorityQueue that's easy to use](https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp/wiki/Using-the-SimplePriorityQueue)
* [I want a PriorityQueue that's as _fast_ as possible](https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp/wiki/Using-the-FastPriorityQueue)
