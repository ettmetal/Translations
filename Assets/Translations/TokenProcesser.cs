using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ettmetal.Translation
{
	public class TokenProcesser {
		private static Regex regexr;
		private Dictionary<string, ITokenResolver> resolvers;

		public TokenProcesser() {
			resolvers = new Dictionary<string, ITokenResolver>();
			regexr = new Regex(Strings.TokenPattern);
		}

		public string Resolve(string token) {
			ITokenResolver resolver = resolvers[token];
			if(resolver == null) {
				string message = string.Format(Strings.NoTokenResolverErrorFormat, token);
				throw new InvalidOperationException(message);
			}
			return resolver.GetTokenValue(token);
		}

		public void Register(string token, ITokenResolver resolver) {
			resolvers.Add(token, resolver);
		}

		public void Register(IEnumerable<string> tokens, ITokenResolver resolver) {
			resolvers.Add(tokens, resolver);
		}

		public string ReplaceTokens(string target) {
			MatchCollection matches = regexr.Matches(target);
			foreach (Match match in matches) {
				string token = match.Groups[1].Value;
				string name = match.Groups[2].Value;
				target = target.Replace(token, Resolve(name));
			}
			return target;
		}
	}
}
