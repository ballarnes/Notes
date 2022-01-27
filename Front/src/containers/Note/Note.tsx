import React from 'react'
import { Alert, Button, Col, Container, FloatingLabel, Form, FormControl, InputGroup, Row, Spinner } from 'react-bootstrap'
import ownTypes from '../../ioc/ownTypes'
import { observer } from 'mobx-react'
import { useInjection } from '../../ioc/ioc.react'
import NoteStore from '../../stores/NoteStore'
import { useTranslation } from 'react-i18next';
import NoteCard from '../../components/NoteCard'

const Note = observer(() => {
  const store = useInjection<NoteStore>(ownTypes.noteStore);
  const { t } = useTranslation();

  return (
    <Container>
      <Row className="justify-content-center">
        <Col lg={4} md={6} xs>
          <InputGroup className="mb-2">
            <FormControl
              type="number"
              value={store.queryString}
              onChange={(ev)=> {store.changeQueryString(ev.target.value)}}
              isInvalid={!!store.error}
              placeholder={t('enterNote')}
            />
            <Button
              id="button"
              disabled={!store.queryString}
              variant="primary"
              onClick={store.search}
              type="button"
            >
              {store.isLoading ? (
                <Spinner animation="border" size="sm" />
              ) : (
                `${t('submit')}`
              )}
            </Button>
          </InputGroup>
          {!!store.error && (
            <p style={{ color: 'red', fontSize: 14 }}>{store.error}</p>
          )}
          <NoteCard note={store.note} />
          {!!store.note && (
            <div className="d-grid gap-2 mt-3"><Button variant="outline-danger" onClick={store.delete}>{t('delete')}</Button></div>
          )}
          {!!store.isDeleted && (
            <Alert variant="danger" className="mt-3">
            {store.result}
          </Alert>
          )}
        </Col>
        {!!store.note && (
          <Col lg={4} md={6} xs>
          <h2 className='text-center'>{t('title')}</h2>
          <Form onSubmit={(ev)=>{ ev.preventDefault();
                                  store.update();
                                }}>
            <FloatingLabel
              controlId="floatingInput"
              label={t('titlenote')}
              className="mb-3"
            >
              <Form.Control
              type="text"
              placeholder={t('titlenote')}
              value={store.title}
              onChange={(ev)=> {store.changeTitle(ev.target.value)}} />
            </FloatingLabel>
            <FloatingLabel
              controlId="floatingTextarea"
              label={t('description')}
              className="mb-3">
              <Form.Control
                as="textarea"
                placeholder={t('description')}
                style={{ height: '100px' }}
                onChange={(ev)=> {store.changeDescription(ev.target.value)}} />
            </FloatingLabel>
            <FloatingLabel
              controlId="floatingInput"
              label={t('author')}
              className="mb-3"
            >
              <Form.Control
              type="author"
              placeholder={t('author')}
              value={store.author}
              onChange={(ev)=> {store.changeAuthor(ev.target.value)}} />
            </FloatingLabel>
            {!!store.error && (
              <p style={{ color: 'red', fontSize: 14 }}>{store.error}</p>
            )}
            <div className="d-grid gap-2">
            <Button variant="outline-success" type="submit">
              {store.isLoading ? (
                <Spinner animation="border" size="sm" />
              ) : (
                `${t('submit')}`
              )}
            </Button>
            </div>
            {!!store.isUpdated && (
            <Alert variant="warning" className="mt-3">
            {store.result}
            </Alert>
            )}
          </Form>
          </Col>
        )}
      </Row>
    </Container>
  )
});

export default Note
