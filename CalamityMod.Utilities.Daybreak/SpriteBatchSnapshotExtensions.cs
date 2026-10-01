using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak;

internal static class SpriteBatchSnapshotExtensions
{
	extension(SpriteBatch sb)
	{
		public void End(out SpriteBatchSnapshot ss)
		{
			ss = new SpriteBatchSnapshot(sb);
			sb.End();
		}

		public void Begin(in SpriteBatchSnapshot ss)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			sb.Begin(ss.SortMode, ss.BlendState, ss.SamplerState, ss.DepthStencilState, ss.RasterizerState, ss.CustomEffect, ss.TransformMatrix);
		}

		public void Restart(in SpriteBatchSnapshot ss)
		{
			sb.End();
			sb.Begin(in ss);
		}
	}
}
