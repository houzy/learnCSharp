using UnityEngine;
using System;
using System.Collections;

public class LearnDelegate : MonoBehaviour {

	// 委托的本质是定义了一个新类。委托实现为派生自基类System.MulticastDelegate的类，
	// System.MulticastDelegate又派生自基类System.Delegate。
	// C#编译器能识别这个类，会使用其委托语法。
	delegate double ProcessDoubleDelegate(double param1, double param2);
	int i = int.Parse("99");

	private double Multiply (double param1, double param2) {
		Debug.Log ("Multiply: " + param1 + " " + param2);
		return param1 * param2;
	}

	private double Divide(double param1, double param2) {
		Debug.Log ("Divide: " + param1 + " " + param2);
		return param1 / param2;
	}

	private void UseFuncExample(Func<double, double, double> action, double param1, double param2) {
		double result = action (param1, param2);
		Debug.Log ("UseFuncExample return == " + result);
	}

	// Use this for initialization
	void Start () {
		Debug.Log(i);

		ProcessDoubleDelegate processDouble;
		processDouble = new ProcessDoubleDelegate (Multiply);
		Debug.Log ("Multiply == " + processDouble(20.5d, 10d));

		processDouble = new ProcessDoubleDelegate (Divide);
		Debug.Log ("Divide == " + processDouble(20.5d, 10d));

		// 委托推断。简化代码编写，直接赋值，等效于上面通过new方法传递参数生成实例。
		processDouble = Multiply;
		Debug.Log ("All == " + processDouble(20.5d, 10d));

		// 多播委托。调用一个委托会顺序调用多个方法。
		processDouble = Multiply;
		processDouble += Divide;
		Debug.Log ("All == " + processDouble(20.5d, 10d));
		// 还可以进行+,-,-=等操作
		ProcessDoubleDelegate processDoubleMultiply = new ProcessDoubleDelegate (Multiply);
		ProcessDoubleDelegate processDoubleDivide = new ProcessDoubleDelegate (Divide);
		ProcessDoubleDelegate processDoubleAll = processDoubleMultiply + processDoubleDivide;
		Debug.Log ("All == " + processDoubleAll(20.5d, 10d));

		// Action<T>和Func<T>泛型委托
		// Action<T>委托表示引用一个void返回类型的方法
		// Func<T>委托表示引用有返回值得方法，其中Func<in T, out TResult>，最后一个表示返回值
		Func<double, double, double>[] operations = {
			Multiply,
			Divide
		};
		foreach (var action in operations) {
			UseFuncExample (action, 20.5d, 10d);
		}

		// 匿名方法
		string tail = " part tail";
		Func<string, string> anonDel = delegate(string param) {
			param += tail;
			return param;
		};
		Debug.Log (anonDel("Part"));

		// 更优雅的匿名方法：Lambda表达式
		// 多个参数时，使用圆括号括起来
		Func<double, double, double> lambdaMultiply = (param1, param2) => {
			return param1 * param2;
		};
		Debug.Log (lambdaMultiply(100, 200));
		// 一个参数时，可以省略圆括号，没有参数时使用空的圆括号()=>
		Func<string, string> lambda = param => {
			param += tail;
			return param;
		};
		Debug.Log (lambda("lambda"));

		// 当表达式只有一条语句时，可以简写，编译器会自动添加return语句，否则必须加花括号
		Func<string, string> simpleLambda = param => param += tail;
		Debug.Log (simpleLambda("simpleLambda"));

		// 闭包
		// 上面通过lambda表达式可以访问lambda表达式外部的变量（string tail），这称为闭包。
		// C#闭包能访问代码块是因为lambda表达式本质上是在构造一个匿名类，同时将
		// 外部变量通过构造函数传递给这个匿名类。构造函数是根据外部传递进来的变量生成的。
		// 在调用lambda表达式时，会创建匿名类的一个实例，并传递调用该方法时变量的值。
		// 所以在调用时要注意当外部变量有变化时，会对lambda表达式有影响。

		// 事件
		// 订阅事件
		Subs ();
		publisher.SendEvent ();
	}

	// 事件
	private Publisher publisher = new Publisher();
	private Subscriber subscriber1 = new Subscriber();
	private Subscriber subscriber2 = new Subscriber();

	// 订阅事件
	private void Subs() {
		publisher.StandardEventName += subscriber1.Receiver;
		publisher.StandardEventName += subscriber2.Receiver;

		publisher.MyEventName += subscriber1.Receiver;
		publisher.MyEventName += subscriber2.Receiver;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
