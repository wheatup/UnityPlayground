using System;
using System.Collections.Generic;

/// <summary>
/// 简易事件系统
/// </summary>
public static class Whevent {
	internal class EventObject {
		public bool once;
		public Action<object> function;
	}

    private static Dictionary<string, List<EventObject>> callStacks = new Dictionary<string, List<EventObject>>();

    public static void Bind(string signal, Action<object> func, bool once = false, bool priority = false) {
		if (!callStacks.ContainsKey(signal)) {
			callStacks.Add(signal, new List<EventObject>());
		}
		EventObject eo = new EventObject();
		eo.once = once;
		eo.function = func;
		if (priority) {
			callStacks[signal].Insert(0, eo);
		} else {
			callStacks[signal].Add(eo);
		}
	}

	public static void Unbind(string signal, Action<object> func){
		if (!callStacks.ContainsKey(signal)) {
			return;
		}

		for (int i = 0; i < callStacks[signal].Count; i++) {
			if(callStacks[signal][i].function == func) {
				callStacks[signal].RemoveAt(i);
				i--;
			}
		}
	}

	public static void Destory(string signal){
		callStacks.Remove(signal);
	}

	public static void Call(string signal, object arg) {
		if (!callStacks.ContainsKey(signal)) return;
		for (int i = 0; i < callStacks[signal].Count; i++) {
			EventObject eo = callStacks[signal][i];
			eo.function.Invoke(arg);
			if (eo.once) {
				callStacks[signal].RemoveAt(i);
				i--;
			}
		}
	}
}
