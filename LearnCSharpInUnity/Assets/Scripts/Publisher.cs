using System;
using System.Collections;

public class Publisher {
	// 事件
	// C#中的事件处理实际上是一种具有特殊签名的delegate。
	// 可以使用标准的EventHandler来声明委托，标准的EventHandler有两种声明，都在System空间中，
	// 前者基于委托，.NET 1.0内部定义了几百个委托，后者基于泛型，不在需要委托了（见C#高级编程第九版8.4.1中的说明），最好使用后者：
	// public delegate void EventHandler(object sender, EventArgs e);
	// public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
	// EventArgs是一个标准类（System空间），不包含任何数据，需要传递数据时，需要派生此类，增加自定义数据。
	// 像下面这个样子：
	// public delegate void MyEventHandler(object sender, MyEventArgs e);
	// public delegate void MyEventHandler< TEventArgs >(object sender, TEventArgs e);
	// 其中的两个参数，sender代表事件发送者，e是事件参数类。MyEventArgs类用来包含与事件相关的数据，
	// 所有的事件参数类都必须从 System.EventArgs类派生。当然，如果你的事件不含参数，那么可以直接
	// 用System.EventArgs类作为参数。
	// 例子，一个使用标准泛型，一个使用自定义泛型，为了简化起见，没有派生EventArgs，直接使用。

	// 标准
	public event EventHandler<EventArgs> StandardEventName;
	// 自定义
	public delegate void MyEventHandler<MyEventArgs>(object sender, MyEventArgs e);
	public event MyEventHandler<EventArgs> MyEventName;

	// 触发事件
	public void SendEvent() {
		var sandardEventName = StandardEventName;
		if (sandardEventName != null) {
			sandardEventName (this, new EventArgs());
		}

		var myEventName = MyEventName;
		if (myEventName != null) {
			myEventName (this, new EventArgs());
		}
	}
}
