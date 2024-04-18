import { useTranslations } from "next-intl";

export default function Reviews() {
  const t = useTranslations("Reviews");

  return (
    <section id="testimonials" className="py-10 lg:py-20">
      <div className="container mx-auto">
        <div className="mx-auto mb-[60px] max-w-[485px] text-center">
          <h2 className="text-dark mb-3 text-3xl leading-[1.2] font-bold sm:text-4xl md:text-[40px]">
            {t("Что говорят наши клиенты")}
          </h2>
        </div>
        <div className="flex flex-col md:flex-row md:-mx-3">
          <div className="flex-1 px-3">
            <div className="p-12 rounded-lg border border-solid border-gray-200 mb-8">
              <p className="text-xl font-semibold">
                Профессиональный сервис и оперативность
              </p>
              <p className="mt-6">
                Очень довольна опытом работы со страховой компанией "НСК".
                Оформление полиса прошло быстро и без лишних сложностей. В
                случае страхового случая, сотрудники компании оперативно
                реагировали и помогли с оформлением необходимых документов.
                Рекомендую "НСК" как надежного партнера в вопросах страхования.
              </p>
              <div className="flex items-center mt-8">
                <img
                  className="w-12 h-12 mr-4 rounded-full"
                  src="https://placeimg.com/150/150/people"
                  alt="Jane Doe"
                />
                <div>
                  <p>Анна Иванова</p>
                  <p className="text-sm text-gray-600">Менеджер по продажам</p>
                </div>
              </div>
            </div>
          </div>
          <div className="flex-1 px-3">
            <div className="p-12 rounded-lg border border-solid border-gray-200 mb-8">
              <p className="text-xl font-semibold">
                Долгосрочное сотрудничество с удовольствием
              </p>
              <p className="mt-6">
                Пользуюсь услугами страховой компании "НСК" уже несколько лет и
                всегда остаюсь довольным уровнем сервиса. Гибкая система тарифов
                позволяет подобрать оптимальные условия страхования под свои
                потребности. В случае возникновения проблем, сотрудники компании
                всегда готовы оказать поддержку и предоставить необходимую
                информацию. Спасибо за профессионализм!
              </p>
              <div className="flex items-center mt-8">
                <img
                  className="w-12 h-12 mr-4 rounded-full"
                  src="https://placeimg.com/150/150/people"
                  alt="John Doe"
                />
                <div>
                  <p>Алексей Петров</p>
                  <p className="text-sm text-gray-600">
                    Руководитель отдела закупок
                  </p>
                </div>
              </div>
            </div>
          </div>
          <div className="flex-1 px-3">
            <div className="p-12 rounded-lg border border-solid border-gray-200 mb-8">
              <p className="text-xl font-semibold">
                Индивидуальный подход и оперативность
              </p>
              <p className="mt-6">
                Страховая компания "НСК" произвела на меня положительное
                впечатление своим индивидуальным подходом к клиентам. Оформление
                страховки было простым и понятным процессом. Важно отметить, что
                при возникновении страхового случая, выплаты производились
                оперативно и без лишних замедлений. Рекомендую "НСК" как
                надежного партнера для защиты своих интересов.
              </p>
              <div className="flex items-center mt-8">
                <img
                  className="w-12 h-12 mr-4 rounded-full"
                  src="https://placeimg.com/150/150/people"
                  alt="Jane Smith"
                />
                <div>
                  <p>Денис Смирнов</p>
                  <p className="text-sm text-gray-600">Финансовый аналитик</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}
