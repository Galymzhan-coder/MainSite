import { ProductImageCarousel } from "~/islands/hero-carousel";

export default function HeroSlider() {
  return (
    <section>
      <div id="home" className="relative overflow-hidden">
        <ProductImageCarousel
          className="w-full"
          images={[
            {
              id: "1",
              url: "/images/hero/hero-image.png",
              name: "hero",
            },
            {
              id: "1",
              url: "https://www.nsk.kz/images/eggs.png",
              name: "hero",
            },
            {
              id: "1",
              url: "https://www.nsk.kz/images/popcorn.png",
              name: "hero",
            },
          ]}
          options={{
            loop: true,
          }}
        />
      </div>
    </section>
  );
}
