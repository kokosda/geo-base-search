using GeoBaseSearch.Core.Interfaces;

namespace GeoBaseSearch.Core.ResponseContainers
{
	public sealed class ResponseContainerWithValue<T> : ResponseContainer, IResponseContainerWithValue<T>
	{
		public T? Value { get; private set; }

		public void SetSuccessValue(T value)
		{
			Value = value;
			IsSuccess = true;
		}

		public void SetErrorValue(T value, string errorMessage)
		{
			if (string.IsNullOrWhiteSpace(errorMessage))
				throw new ArgumentNullException(nameof(errorMessage));

			Value = value;
			IsSuccess = false;
			AddErrorMessage(errorMessage);
		}

		public new IResponseContainerWithValue<T> JoinWith(IResponseContainer anotherResponseContainer)
		{
			base.JoinWith(anotherResponseContainer);
			return this;
		}
	}
}
