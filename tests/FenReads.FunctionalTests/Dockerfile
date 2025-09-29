FROM karatelabs/karate:latest

WORKDIR /tests

COPY src/test/resources/ /tests/

ENV KARATE_ENV=docker
ENV KARATE_THREADS=1

VOLUME ["/tests/reports"]

ENTRYPOINT ["java", "-jar", "/karate.jar"]
CMD ["-o", "/tests/reports", "/tests/features"]