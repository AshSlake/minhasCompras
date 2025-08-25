using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minhasCompras.helpers
{
    public static class AnimationHelpers
    {
        // utilizamos o metodo como async para que a animação não bloqueie a thread principal da UI
        public static async Task AnimateClickAsync(View viewToAnimate)
        {
            // Reduz o tamanho da view para 95% em 100 milissegundos com uma animação suave
            await viewToAnimate.ScaleTo(0.95, 100, Easing.CubicInOut);

            // Restaura o tamanho original da view para 100% em 100 milissegundos com uma animação suave
            await viewToAnimate.ScaleTo(1, 100, Easing.CubicIn);
        }
    }
}
