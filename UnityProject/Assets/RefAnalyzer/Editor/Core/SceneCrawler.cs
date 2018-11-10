using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using RefAnalyzer.Validation;

namespace RefAnalyzer.Core {
	public class SceneCrawler {
		public RawData Data { get; }

		string _scenePath;
		
		public SceneCrawler(string scenePath) {
			Guard.NotNullOrWhiteSpace(scenePath);
			Data = new RawData(scenePath);
			_scenePath = scenePath;
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
				if ( !target ) {
					Debug.LogWarningFormat("[{0}] Event target isn't specified for: {1}/{2}", _scenePath, AssetUtils.GetPathTo(source), propertyName);
					continue;
				}
				var methodName = eventValue.GetPersistentMethodName(i);
				try {
					var node = new RawDataNode(source, propertyName, target, methodName);
					Data.AddRef(node);
				} catch ( Exception e ) {
					Debug.LogWarningFormat("[{0}] Error while processing: {1}/{2}: {3}", _scenePath, AssetUtils.GetPathTo(source), propertyName, e);
				}
			}
		}
	}
}