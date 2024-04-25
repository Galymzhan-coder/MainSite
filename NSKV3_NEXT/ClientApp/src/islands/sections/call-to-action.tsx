export default function CallToAction() {
  return (
    <section className="relative z-10 overflow-hidden bg-gradient-to-t from-blue-200 via-blue-500 to-blue-800 py-20 lg:py-[115px]">
      <div className="container mx-auto">
        <div className="relative overflow-hidden">
          <div className="flex flex-wrap items-stretch -mx-4">
            <div className="w-full px-4">
              <div className="mx-auto max-w-[570px] text-center">
                <h2 className="mb-2.5 text-3xl font-bold md:leading-[1.44] text-white md:text-[38px]">
                  <span>Наступил страховой случай?</span>
                </h2>
                <p className="mb-6 mx-auto max-w-[515px] text-base leading-[1.5] text-white">
                  Заполните форму и получите вашу страховую выплату
                </p>
                <a
                  href="javascript:void(0)"
                  className="inline-block py-3 text-base font-medium text-white transition border border-transparent rounded-md bg-secondary px-7 hover:bg-[#0BB489]"
                >
                  Перейти
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div>
        <span className="absolute top-0 left-0">
          <svg
            width="495"
            height="470"
            viewBox="0 0 495 470"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <circle
              cx="55"
              cy="442"
              r="138"
              stroke="white"
              strokeOpacity="0.04"
              strokeWidth="50"
            />
            <circle
              cx="446"
              r="39"
              stroke="white"
              strokeOpacity="0.04"
              strokeWidth="20"
            />
            <path
              d="M245.406 137.609L233.985 94.9852L276.609 106.406L245.406 137.609Z"
              stroke="white"
              strokeOpacity="0.08"
              strokeWidth="12"
            />
          </svg>
        </span>
        <span className="absolute bottom-0 right-0">
          <svg
            width="493"
            height="470"
            viewBox="0 0 493 470"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <circle
              cx="462"
              cy="5"
              r="138"
              stroke="white"
              strokeOpacity="0.04"
              strokeWidth="50"
            />
            <circle
              cx="49"
              cy="470"
              r="39"
              stroke="white"
              strokeOpacity="0.04"
              strokeWidth="20"
            />
            <path
              d="M222.393 226.701L272.808 213.192L259.299 263.607L222.393 226.701Z"
              stroke="white"
              strokeOpacity="0.06"
              strokeWidth="13"
            />
          </svg>
        </span>
      </div>
    </section>
  );
}