using System;

namespace Ettmetal.Translation {
	public class CallbackTokenResolver : ITokenResolver {
		private Func<string> callback;
		private Func<string, string> parameterizedCallback;

		public string GetTokenValue(string token) {
			return callback == null ? parameterizedCallback(token) : callback();
		}

		public CallbackTokenResolver(Func<string> callback) {
			this.callback = callback;
		}

		public CallbackTokenResolver(Func<string, string> callback) {
			parameterizedCallback = callback;
		}
	}
}
