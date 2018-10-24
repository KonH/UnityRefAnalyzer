using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Assertions;

namespace RefAnalyzer.Core {
	public class SceneCrawler {
		public RawData Data { get; }

		public SceneCrawler(string scenePath) {
			Assert.IsTrue(!string.IsNullOrEmpty(scenePath));
			Data = new RawData(scenePath);
		}

		public void Process(Component component) {
			var type = component.GetType();
			var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			foreach ( var field in fields ) {
				Process(component, field);
			}
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach ( var property in properties ) {
				Process(component, property);
			}
		}

		void Process(Component source, FieldInfo field) {
			if ( IsUnityEventType(field.FieldType) ) {
				var value = field.GetValue(source);
				ProcessUnityEvent(source, field.Name, value as UnityEvent);
			}
		}
		
		void Process(Component source, PropertyInfo property) {
			if ( IsUnityEventType(property.PropertyType) ) {
				var value = property.GetValue(source, null);
				ProcessUnityEvent(source, property.Name, value as UnityEvent);
			}
		}

		bool IsUnityEventType(Type type) {
			return type.IsSubclassOf(typeof(UnityEvent));
		}

		void ProcessUnityEvent(Component source, string propertyName, UnityEvent eventValue) {
			var events = eventValue.GetPersistentEventCount();
			for ( var i = 0; i < events; i++ ) {
				var target = eventValue.GetPersistentTarget(i);
				var methodName = eventValue.GetPersistentMethodName(i);
				Data.AddRef(new RawDataNode(source, propertyName, target, methodName));
			}
		}
	}
}