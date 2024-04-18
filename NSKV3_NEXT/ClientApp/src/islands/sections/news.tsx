import { useTranslations } from "next-intl";

export default function News() {
  const t = useTranslations("News");

  return (
    <section className="pb-10 lg:pb-20">
      <div className="container mx-auto">
        <div className="flex flex-wrap justify-center -mx-4">
          <div className="w-full px-4">
            <div className="mx-auto mb-[60px] max-w-[485px] text-center">
              <h2 className="mb-4 text-3xl font-bold text-dark sm:text-4xl md:text-[40px] md:leading-[1.2]">
                {t("Новости и акции")}
              </h2>
            </div>
          </div>
        </div>
        <div className="flex flex-wrap -mx-4">
          <div className="w-full px-4 md:w-1/2 lg:w-1/3">
            <div className="group mb-10">
              <div className="mb-8 overflow-hidden rounded-[5px]">
                <a href="blog-details.html" className="block">
                  <img
                    src="https://www.nsk.kz/media/blogs/2y/1670904080ojbjw_590x393.jpg"
                    alt="image"
                    className="w-full transition group-hover:rotate-6 group-hover:scale-125"
                  />
                </a>
              </div>
              <div>
                <span className="inline-block px-4 py-0.5 mb-6 text-xs font-medium leading-loose text-center text-white rounded-[5px] bg-primary">
                  13 декабря
                </span>
                <h3>
                  <a
                    href="javascript:void(0)"
                    className="inline-block mb-4 text-xl font-semibold text-dark hover:text-primary sm:text-2xl lg:text-xl xl:text-2xl"
                  >
                    Уведомление о выборе страховщика-приобретателя
                  </a>
                </h3>
                <p className="max-w-[370px] text-base text-body-color">
                  Lorem Ipsum is simply dummy text of the printing and
                  typesetting industry.
                </p>
              </div>
            </div>
          </div>
          <div className="w-full px-4 md:w-1/2 lg:w-1/3">
            <div className="group mb-10">
              <div className="mb-8 overflow-hidden rounded-[5px]">
                <a href="blog-details.html" className="block">
                  <img
                    src="https://www.nsk.kz/media/blogs/ea/1672025728mkb1f_590x393.png"
                    alt="image"
                    className="w-full transition group-hover:rotate-6 group-hover:scale-125"
                  />
                </a>
              </div>
              <div>
                <span className="inline-block px-4 py-0.5 mb-6 text-xs font-medium leading-loose text-center text-white rounded-[5px] bg-primary">
                  26 декабря
                </span>
                <h3>
                  <a
                    href="javascript:void(0)"
                    className="inline-block mb-4 text-xl font-semibold text-dark hover:text-primary sm:text-2xl lg:text-xl xl:text-2xl"
                  >
                    Объявление о завершении процедуры передачи страхового
                    портфеля
                  </a>
                </h3>
              </div>
            </div>
          </div>
          <div className="w-full px-4 md:w-1/2 lg:w-1/3">
            <div className="group mb-10">
              <div className="mb-8 overflow-hidden rounded-[5px]">
                <a href="blog-details.html" className="block">
                  <img
                    src="https://www.nsk.kz/media/blogs/8e/1681125914huj3n_590x393.jpeg"
                    alt="image"
                    className="w-full transition group-hover:rotate-6 group-hover:scale-125"
                  />
                </a>
              </div>
              <div>
                <span className="inline-block px-4 py-0.5 mb-6 text-xs font-medium leading-loose text-center text-white rounded-[5px] bg-primary">
                  10 апреля
                </span>
                <h3>
                  <a
                    href="javascript:void(0)"
                    className="inline-block mb-4 text-xl font-semibold text-dark hover:text-primary sm:text-2xl lg:text-xl xl:text-2xl"
                  >
                    Нам исполнилось 27 лет!
                  </a>
                </h3>
                <p className="max-w-[370px] text-base text-body-color">
                  Уважаемые друзья! Сегодня наша компания отмечает свой 27-ой
                  день рождения! 27 лет – это не просто возраст и дата в
                  календаре.
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}
